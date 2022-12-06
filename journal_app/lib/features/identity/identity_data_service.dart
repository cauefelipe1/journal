import 'package:journal_mobile_app/infrastructure/http/authenticated_http_client.dart';
import 'package:journal_mobile_app/models/identity.dart';

abstract class IIdentityDataService {
  Future<UserLoginResult?> login(UserLoginInput input);
}

class IdentityDataService extends AuthenticatedHttpClient implements IIdentityDataService {
  @override
  Future<UserLoginResult?> login(UserLoginInput input) async {
    var result = await loginUser(input);

    return result;
  }
}
