import 'dart:io';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/base_http_client.dart';
import 'package:journal_mobile_app/models/identity.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

class AuthenticatedHttpClient extends BaseHttpClient {
  final String _jwtTokenKey = "user_jwt_token";
  final String _refreshTokenKey = "jwt_refresh_token";

  late Map<String, String> _defaultHeader;

  late final FlutterSecureStorage _secureStorage;

  AuthenticatedHttpClient() {
    _secureStorage = const FlutterSecureStorage();

    _defaultHeader = {HttpHeaders.contentTypeHeader: jsonMimeType};
  }

  Future<dynamic> executeAuthGet(String path) async {
    var headers = await _getHttpHeaders();

    return super.executeGet(path, headers);
  }

  Future<dynamic> executeAuthPost(String path, Object? body) async {
    var headers = await _getHttpHeaders();

    return super.executePost(path, body, headers);
  }

  Future<UserLoginResult?> loginUser(UserLoginInput input) async {
    var headers = await _getHttpHeaders();
    var requestResult = await executePost(ApiConstants.identity.login, input, headers);

    if (requestResult != null) {
      var loginResult = UserLoginResult.fromJson(requestResult);

      await storeTokens(loginResult);

      return loginResult;
    }

    return null;
  }

  Future<void> storeTokens(UserLoginResult loginResult) async {
    if (loginResult.token != null && loginResult.refreshToken != null) {
      var saveJwtFuture = _secureStorage.write(key: _jwtTokenKey, value: loginResult.token);
      var saveRefreshTokenFuture = _secureStorage.write(key: _refreshTokenKey, value: loginResult.refreshToken);

      await Future.wait([saveJwtFuture, saveRefreshTokenFuture]);
    }
  }

  Future<String?> _getJwtToken() async {
    String? token = await _secureStorage.read(key: _jwtTokenKey);

    if (token == null || token.isEmpty) {
      return null;
    }

    if (JwtDecoder.isExpired(token)) {
      token = await _refreshJwtToken(token);
    }

    return token;
  }

  Future<String?> _refreshJwtToken(String jwtToken) async {
    String? refreshToken = await _secureStorage.read(key: _refreshTokenKey);

    if (refreshToken == null) {
      return null;
    }

    var input = RefreshTokenInput(token: jwtToken, refreshToken: refreshToken);

    var requestResult = await executePost(ApiConstants.identity.refreshToken, input, _defaultHeader);

    if (requestResult == null) {
      return null;
    }

    var result = UserLoginResult.fromJson(requestResult);
    await storeTokens(result);

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
