import 'dart:io';

import 'package:journal_mobile_app/config/app_config.dart';

import 'locator.dart';
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

void _initializeAppConfigs() async {
  const String env = String.fromEnvironment(
    "ENVIRIONMENT",
    defaultValue: "local",
  );

  var _ = await AppConfig.forEnvironment(env);
}

void main() async {
  if (kDebugMode) {
    HttpOverrides.global = MyHttpOverrides();
  }

  WidgetsFlutterBinding.ensureInitialized();

  _initializeAppConfigs();

  setupLocator();

  //As the application is using the Riverpod, it must be wraped in a ProviderScope
  runApp(const ProviderScope(
    child: Application(),
  ));
}
