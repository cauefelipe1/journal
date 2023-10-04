import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/identity.dart';
import 'package:journal_mobile_app/models/user.dart';

final identityServiceProvider = Provider<IIdentityService>((ref) {
  return IdentityService(httpClient: ref.watch(httpClientProvider));
});

abstract class IIdentityService {
  Future<UserLoginResult> loginUser(UserLoginInput input);
  Future<UserData?> getUserData();
  Future<bool> checkIfAuthenticated();
  Future<bool> logoutUser();
}

class IdentityService implements IIdentityService {
  late final IAuthenticatedHttpClient _httpClient;

  IdentityService({required IAuthenticatedHttpClient httpClient}) {
    _httpClient = httpClient;
  }

  @override
  Future<UserLoginResult> loginUser(UserLoginInput input) async {
    var loginResult = await _httpClient.loginUser(input);

    if (loginResult != null) {
      return loginResult;
    } else {
      return UserLoginResult(
        token: null,
        refreshToken: null,
        errors: ["Error when attempting to login."],
      ); //TODO: Translate
    }
  }

  @override
  Future<UserData?> getUserData() async {
    var requestResult = await _httpClient.executeAuthGet(ApiConstants.identity.userData);

    if (requestResult == null) {
      return null;
    }

    var userData = UserData.fromJson(requestResult["data"]);

    return userData;
  }

  @override
  Future<bool> checkIfAuthenticated() async {
    var requestResult = await _httpClient.executeAuthGet(ApiConstants.identity.checkIfAuthenticated);

    return requestResult != null;
  }

  @override
  Future<bool> logoutUser() async {
    bool loginResult = await _httpClient.logoutUser();

    return loginResult;
  }
}
