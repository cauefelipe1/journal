import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/identity.dart';
import 'package:journal_mobile_app/models/user.dart';

class IdentityService {
  late final AuthenticatedHttpClient _httpClient;

  IdentityService() {
    _httpClient = AuthenticatedHttpClient();
  }

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

  Future<UserData?> getUserData() async {
    var requestResult = await _httpClient.executeAuthGet(ApiConstants.identity.userData);

    if (requestResult == null) {
      return null;
    }

    var userData = UserData.fromJson(requestResult);

    return userData;
  }
}
