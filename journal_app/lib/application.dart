import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/gui/components/loading_overlay.dart';
import 'package:journal_mobile_app/gui/home/home_page.dart';
import 'package:journal_mobile_app/gui/identity/login_page.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';

class Application extends ConsumerWidget {
  final isLoggedIn;
  const Application({Key? key, required bool this.isLoggedIn}) : super(key: key);

  static const String _title = 'Journal';

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    //TODO: Should this be in a FutureBuilder?

    SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle.light);
    SystemChrome.setPreferredOrientations([DeviceOrientation.portraitUp]);

    return MaterialApp(
      title: _title,
      onGenerateTitle: (context) => context.l10n.appName,
      localizationsDelegates: AppLocalizations.localizationsDelegates,
      supportedLocales: AppLocalizations.supportedLocales,
      debugShowCheckedModeBanner: false,
      theme: _getThemeData(),
      home: isLoggedIn ? HomePage() : LoadingOverlay(child: LoginPage()),
    );
  }

  ThemeData _getThemeData() => ThemeData(
        fontFamily: 'OpenSans',
        primaryColor: Colors.teal[300],
        primarySwatch: Colors.teal,
        appBarTheme: const AppBarTheme(
          systemOverlayStyle: SystemUiOverlayStyle.dark,
        ),
        elevatedButtonTheme: ElevatedButtonThemeData(
          style: ButtonStyle(
            shape: MaterialStatePropertyAll(
              RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(5.0),
              ),
            ),
          ),
        ),
      );
}
