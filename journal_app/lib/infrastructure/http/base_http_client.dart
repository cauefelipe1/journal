import 'dart:convert';
import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:http/http.dart' as http;
import 'package:journal_mobile_app/config/app_config.dart';
import 'package:journal_mobile_app/infrastructure/http/error_hanlder/http_error_handler.dart';

abstract class BaseHttpClient {
  String jsonMimeType = 'application/json; charset=UTF-8';

  @protected
  IHttpErrorHandler httpErrorHandler;

  BaseHttpClient({required this.httpErrorHandler});

  @protected
  Future<dynamic> executeGet(String path, Map<String, String>? headers) async {
    try {
      var url = Uri.parse("${AppConfig.instance.apiUrl}/$path");
      var response = await http.get(url, headers: headers);

      var result = _internalGetRequestResult(response);

      return result;
    } on Exception catch (e) {
      bool handled = _handleHttpException(e);

      if (!handled) {
        rethrow;
      }
    }
  }

  @protected
  Future<dynamic> executePost(String path, Object? body, Map<String, String>? headers) async {
    try {
      var url = Uri.parse("${AppConfig.instance.apiUrl}/$path");
      var response = await http.post(url, headers: headers, body: json.encode(body));

      var result = _internalGetRequestResult(response);

      return result;
    } on Exception catch (e) {
      bool handled = _handleHttpException(e);

      if (!handled) {
        rethrow;
      }

      return null;
    }
  }

  dynamic _internalGetRequestResult(http.Response response) {
    if (_isHttpStatusCodeSuccess(response.statusCode)) {
      return json.decode(response.body);
    }

    return null;
  }

  bool _isHttpStatusCodeSuccess(int statusCode) => (statusCode >= 200 && statusCode <= 299);

  bool _handleHttpException(Exception e) {
    if (e is SocketException) {
      httpErrorHandler.SocketClientException(e);

      return true;
    }

    return false;
  }
}
