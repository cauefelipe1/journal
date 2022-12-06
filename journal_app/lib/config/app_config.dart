import 'dart:convert';
import 'dart:io' show Platform;

import 'package:flutter/services.dart';

class AppConfig {
  final String apiUrl;

  static AppConfig? _config;

  AppConfig.privateConstructor({required this.apiUrl});

  static AppConfig get instance {
    if (_config == null) {
      throw Exception("The App config was not initialized");
    }

    return _config!;
  }

  static Future<AppConfig> forEnvironment(String? env) async {
    if (_config == null) {
      env = env ?? "local";

      String filePath = "assets/config/appConfig.$env.json";

      final jsonConfigContents = await rootBundle.loadString(filePath);

      final json = jsonDecode(jsonConfigContents);

      String url = Platform.isAndroid && json["androidApiUrl"] != null ? json["androidApiUrl"] : json["apiUrl"];

      _config = AppConfig.privateConstructor(apiUrl: url);
    }

    return _config!;
  }
}
