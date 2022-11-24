import 'package:flutter/material.dart';
import 'package:journal_mobile_app/features/identity/identity_data_service.dart';

class LoginViewModel extends ChangeNotifier {
  final IIdentityDataService identityDS;

  LoginViewModel({required this.identityDS});
}
