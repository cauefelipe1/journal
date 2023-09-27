import 'package:flutter/material.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';

class ErrorDialog {
  static Future<void> show(BuildContext ownerContext, String title, String errorMessage) {
    return showDialog<void>(
      context: ownerContext,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(title),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                Text(errorMessage),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: Text(context.l10n.ok),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }
}
