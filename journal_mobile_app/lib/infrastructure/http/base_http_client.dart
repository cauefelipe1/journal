import 'dart:convert';
import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:http/http.dart' as http;
import 'package:meta/meta.dart';

abstract class BaseHttpClient {
  final String _jsonMimeType = 'application/json; charset=UTF-8';

  @protected
  Future<dynamic> executeGet(String path) async {
    try {
      var url = Uri.parse(path);
      var response = await http.get(url, headers: _internalGetHeaders());

      var result = _internalGetRequestResult(response);

      return result;
    } catch (e) {
      if (kDebugMode) rethrow;

      return null;
    }
  }

  @protected
  Future<dynamic> executePost(String path, Object? body) async {
    try {
      var url = Uri.parse(path);
      var response = await http.post(url,
          headers: _internalGetHeaders(), body: json.encode(body));

      var result = _internalGetRequestResult(response);

      return result;
    } catch (e) {
      if (kDebugMode) rethrow;

      return null;
    }
  }

  Map<String, String> _internalGetHeaders() {
    return {
      HttpHeaders.authorizationHeader: 'Bearer your_api_token_here',
      HttpHeaders.contentTypeHeader: _jsonMimeType
    };
  }

  dynamic _internalGetRequestResult(http.Response response) {
    if (response.statusCode == 200) {
      return json.decode(response.body);
    }

    return null;
  }
}
