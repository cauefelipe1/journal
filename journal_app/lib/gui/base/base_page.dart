import 'package:flutter/material.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

abstract class BasePage extends StatefulWidget {
  const BasePage({super.key});
}

abstract class BasePageState<T extends StatefulWidget> extends State<T> {
  @override
  Widget build(BuildContext context) {
    var l10n = AppLocalizations.of(context)!;

    return widgetBuild(context, l10n);
  }

  Widget widgetBuild(BuildContext context, AppLocalizations l10n);
}
