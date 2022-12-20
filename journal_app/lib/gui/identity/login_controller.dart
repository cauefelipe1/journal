import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/features/identity/identity_service.dart';
import 'package:journal_mobile_app/models/identity.dart';

final loginControllerProvider = Provider((ref) {
  return LoginController(identityDS: ref.watch(identityServiceProvider));
});

class LoginController {
  late final IIdentityService _identityDS;

  LoginController({required IIdentityService identityDS}) {
    _identityDS = identityDS;
  }

  Future<UserLoginResult> loginUser(String username, String password) async {
    UserLoginResult loginResult;

    var loginInput = UserLoginInput(email: username, password: password);

    loginResult = await _identityDS.loginUser(loginInput);

    return loginResult;
  }
}
