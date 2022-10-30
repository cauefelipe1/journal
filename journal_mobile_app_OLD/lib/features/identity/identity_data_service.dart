import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/identity.dart';

class IdentityDataService extends AuthenticatedHttpClient {
  Future<UserLoginResult> login(UserLoginInput input) async {
    var result = await loginUser(input);

    if (result != null) {
      return result;
    } else {
      return UserLoginResult(
          token: null,
          refreshToken: null,
          errors: ["Error when attempting to login."]); //TODO: Translate
    }
  }
}
