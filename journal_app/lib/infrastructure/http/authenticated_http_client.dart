import 'dart:io';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/base_http_client.dart';
import 'package:journal_mobile_app/infrastructure/http/error_hanlder/http_error_handler.dart';
import 'package:journal_mobile_app/l10n/app_localization_provider.dart';
import 'package:journal_mobile_app/models/identity.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

final httpClientProvider = Provider<IAuthenticatedHttpClient>((ref) {
  final String locale = ref.watch(appLocalizationsProvider).localeName;
  final IHttpErrorHandler errorHandler = ref.watch(httpErrorHandlerProvider);

  return AuthenticatedHttpClient(languageCode: locale, httpErrorHandler: errorHandler);
});

abstract class IAuthenticatedHttpClient {
  Future<UserLoginResult?> loginUser(UserLoginInput input);
  Future<bool> logoutUser();
  Future<dynamic> executeAuthGet(String path);
  Future<dynamic> executeAuthPost(String path, Object? body);
}

class AuthenticatedHttpClient extends BaseHttpClient implements IAuthenticatedHttpClient {
  static const String _JWT_TOKEN_KEY = "user_jwt_token";
  static const String _REFRESH_TOKEN_KEY = "jwt_refresh_token";
  final String languageCode;

  late Map<String, String> _defaultHeader;

  late final FlutterSecureStorage _secureStorage;

  AuthenticatedHttpClient({
    required this.languageCode,
    required IHttpErrorHandler httpErrorHandler,
  }) : super(httpErrorHandler: httpErrorHandler) {
    _secureStorage = const FlutterSecureStorage();

    _defaultHeader = {
      HttpHeaders.contentTypeHeader: jsonMimeType,
      HttpHeaders.acceptLanguageHeader: languageCode,
    };
  }

  @override
  Future<dynamic> executeAuthGet(String path) async {
    var headers = await _getHttpHeaders();

    return super.executeGet(path, headers);
  }

  @override
  Future<dynamic> executeAuthPost(String path, Object? body) async {
    var headers = await _getHttpHeaders();

    return super.executePost(path, body, headers);
  }

  @override
  Future<UserLoginResult?> loginUser(UserLoginInput input) async {
    var headers = await _getHttpHeaders();
    var requestResult = await executePost(ApiConstants.identity.login, input, headers);

    if (requestResult != null) {
      var loginResult = UserLoginResult.fromJson(requestResult);

      await _storeTokens(loginResult);

      return loginResult;
    }

    return null;
  }

  @override
  Future<bool> logoutUser() async {
    var headers = await _getHttpHeaders();
    var requestResult = await executePost(ApiConstants.identity.logoutUser, null, headers);

    if (requestResult["data"]) {
      await _cleanTokens();

      return true;
    }

    return false;
  }

  Future<void> _storeTokens(UserLoginResult loginResult) async {
    if (loginResult.token != null && loginResult.refreshToken != null) {
      var saveJwtFuture = _secureStorage.write(key: _JWT_TOKEN_KEY, value: loginResult.token);
      var saveRefreshTokenFuture = _secureStorage.write(key: _REFRESH_TOKEN_KEY, value: loginResult.refreshToken);

      await Future.wait([saveJwtFuture, saveRefreshTokenFuture]);
    }
  }

  Future<void> _cleanTokens() async {
    var saveJwtFuture = _secureStorage.delete(key: _JWT_TOKEN_KEY);
    var saveRefreshTokenFuture = _secureStorage.delete(key: _REFRESH_TOKEN_KEY);

    await Future.wait([saveJwtFuture, saveRefreshTokenFuture]);
  }

  Future<String?> _getJwtToken() async {
    String? token = await _secureStorage.read(key: _JWT_TOKEN_KEY);

    if (token == null || token.isEmpty) {
      return null;
    }

    if (JwtDecoder.isExpired(token)) {
      token = await _refreshJwtToken(token);
    }

    return token;
  }

  Future<String?> _refreshJwtToken(String jwtToken) async {
    String? refreshToken = await _secureStorage.read(key: _REFRESH_TOKEN_KEY);

    if (refreshToken == null) {
      return null;
    }

    var input = RefreshTokenInput(token: jwtToken, refreshToken: refreshToken);

    var requestResult = await executePost(ApiConstants.identity.refreshToken, input, _defaultHeader);

    if (requestResult == null) {
      return null;
    }

    var result = UserLoginResult.fromJson(requestResult);
    await _storeTokens(result);

    return result.token;
  }

  Future<Map<String, String>> _getHttpHeaders() async {
    var tokenFuture = _getJwtToken();
    var headers = Map<String, String>.from(_defaultHeader);

    String? token = await tokenFuture;

    if (token != null) {
      headers[HttpHeaders.authorizationHeader] = 'Bearer $token';
    }

    return headers;
  }
}
