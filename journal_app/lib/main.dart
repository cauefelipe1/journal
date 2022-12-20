import 'dart:io';

import 'package:flutter/services.dart';
import 'package:journal_mobile_app/config/app_config.dart';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/application.dart';

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

  await Future.wait([_initializeAppConfigs(), _addFontsLicense()]);

  //As the application is using the Riverpod, it must be wraped in a ProviderScope
  runApp(const ProviderScope(
    child: Application(),
  ));
}
