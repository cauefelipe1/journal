import 'package:journal_mobile_app/infrastructure/http/api_constants.dart';
import 'package:journal_mobile_app/infrastructure/http/base_http_client.dart';
import 'package:journal_mobile_app/models/identity.dart';

class IdentityDataService extends BaseHttpClient {
  Future<UserLoginResult> login(UserLoginInput input) async {
    var loginResult = await executePost(ApiConstants.identity.login, input);

    if (loginResult != null) {
      var result = UserLoginResult.fromJson(loginResult);

      return result;
    }

    return UserLoginResult(
        token: null,
        refreshToken: null,
        errors: ["Error when attempting to login."]); //TODO: Translate
  }
}
