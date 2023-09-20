import 'dart:io';

import 'package:flutter/services.dart';
import 'package:journal_mobile_app/config/app_config.dart';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/application.dart';
import 'package:journal_mobile_app/features/identity/identity_service.dart';

class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    var client = super.createHttpClient(context);
    client.badCertificateCallback = (X509Certificate cert, String host, int port) => true;

    return client;
  }
}

Future<AppConfig> _initializeAppConfigs() async {
  const String env = String.fromEnvironment(
    "ENVIRIONMENT",
    defaultValue: "local",
  );

  return AppConfig.forEnvironment(env);
}

Future _addFontsLicense() async {
  LicenseRegistry.addLicense(() async* {
    final openSansLicense = rootBundle.loadString('assets/fonts/open_sasns/OFL.txt');
    final montserratLicense = rootBundle.loadString('assets/fonts/montserrat/OFL.txt');
    var licenses = await Future.wait([openSansLicense, montserratLicense]);

    yield LicenseEntryWithLineBreaks(['open_sans_font'], licenses[0]);
    yield LicenseEntryWithLineBreaks(['montserrat_font'], licenses[1]);
  });
}

void main() async {
  if (kDebugMode) {
    HttpOverrides.global = MyHttpOverrides();
  }

  WidgetsFlutterBinding.ensureInitialized();

  final appConfigFuture = _initializeAppConfigs();
  final fontsLicensesFuture = _addFontsLicense();

  final container = ProviderContainer();

  bool isLogged = false;

  try {
    final identityService = container.read(identityServiceProvider);
    isLogged = await identityService.checkIfAuthenticated();
  } catch (e) {
    isLogged = false;
  }

  await Future.wait([appConfigFuture, fontsLicensesFuture]);

  //As the application is using the Riverpod, it must be wraped in a ProviderScope
  //But here we are using the UncontrolledProviderScope because we are consumning a provider before flutter is initialized.
  runApp(UncontrolledProviderScope(
    container: container,
    child: Application(
      isLoggedIn: isLogged,
    ),
  ));
}
