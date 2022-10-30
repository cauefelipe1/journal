import 'package:journal_mobile_app/features/identity/identity_data_service.dart';
import 'package:journal_mobile_app/models/identity.dart';

class IdentityService {
  late final IdentityDataService _dataService;

  IdentityService() {
    _dataService = IdentityDataService();
  }

  Future<UserLoginResult> loginUser(UserLoginInput input) async {
    var loginResult = await _dataService.login(input);

    return loginResult;
  }
}
