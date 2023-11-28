import 'dart:io';

import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/infrastructure/http/error_hanlder/http_error_handler_android.dart';
import 'package:journal_mobile_app/infrastructure/http/error_hanlder/http_error_handler_ios.dart';
import 'package:journal_mobile_app/infrastructure/http/error_hanlder/http_error_handler_macos.dart';
import 'package:journal_mobile_app/infrastructure/http/http_error.dart';

final httpErrorHandlerProvider = Provider<IHttpErrorHandler>((ref) {
  if (Platform.isMacOS) {
    return new HttpErrorHandlerMacOS();

  } else if (Platform.isIOS){
    return new HttpErrorHandlerIos();

  } else if (Platform.isAndroid){
    return new HttpErrorHandlerAndroid();
    
  }

  throw Exception("Platform not supported.");
});

abstract class IHttpErrorHandler {
  HttpError SocketClientException(SocketException e);
}
