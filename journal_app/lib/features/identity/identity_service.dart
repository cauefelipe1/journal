import 'package:journal_mobile_app/features/identity/identity_data_service.dart';
import 'package:journal_mobile_app/locator.dart';
import 'package:journal_mobile_app/models/identity.dart';

class IdentityService {
  late final IIdentityDataService _dataService;

  IdentityService() {
    _dataService = locator<IIdentityDataService>();
  }

  Future<UserLoginResult> loginUser(UserLoginInput input) async {
    var loginResult = await _dataService.login(input);

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
}
