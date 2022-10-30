import 'dart:convert';

import 'package:flutter/foundation.dart';
import 'package:http/http.dart' as http;

abstract class BaseHttpClient {
  String jsonMimeType = 'application/json; charset=UTF-8';

  @protected
  Future<dynamic> executeGet(String path, Map<String, String>? headers) async {
    try {
      var url = Uri.parse(path);
      var response = await http.get(url, headers: headers);

      var result = _internalGetRequestResult(response);

      return result;
    } catch (e) {
      if (kDebugMode) {
        rethrow;
      }

      return null;
    }
  }

  @protected
  Future<dynamic> executePost(
      String path, Object? body, Map<String, String>? headers) async {
    try {
      var url = Uri.parse(path);
      var response =
          await http.post(url, headers: headers, body: json.encode(body));

      var result = _internalGetRequestResult(response);

      return result;
    } catch (e) {
      if (kDebugMode) {
        rethrow;
      }

      return null;
    }
  }

  dynamic _internalGetRequestResult(http.Response response) {
    if (response.statusCode == 200) {
      return json.decode(response.body);
    }

    return null;
  }
}
