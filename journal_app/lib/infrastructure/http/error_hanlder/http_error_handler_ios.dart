import 'dart:io';

import 'package:journal_mobile_app/infrastructure/http/error_hanlder/http_error_handler.dart';
import 'package:journal_mobile_app/infrastructure/http/http_error.dart';

class HttpErrorHandlerIos implements IHttpErrorHandler {
  @override
  HttpError SocketClientException(SocketException e) {
    return InternalHandleSocketClientException(e);
  }

  HttpError InternalHandleSocketClientException(SocketException e) {
    if (e.osError == null) {
      return HttpError.unknown;
    }

    switch (e.osError!.errorCode) {
      case 61:
        return HttpError.hostUnavailable;
    }

    return HttpError.unknown;
  }
}
