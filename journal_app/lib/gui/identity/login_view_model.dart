import 'package:flutter/material.dart';
import 'package:journal_mobile_app/features/identity/identity_service.dart';

class LoginViewModel extends ChangeNotifier {
  final IdentityService identityDS;

  LoginViewModel({required this.identityDS});
}
