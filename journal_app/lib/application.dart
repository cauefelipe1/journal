import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:journal_mobile_app/gui/components/loading_overlay.dart';
import 'package:journal_mobile_app/gui/identity/login_page.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

class Application extends StatelessWidget {
  const Application({Key? key}) : super(key: key);

  static const String _title = 'Journal';

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle.dark);
    SystemChrome.setPreferredOrientations([DeviceOrientation.portraitUp]);

    return MaterialApp(
      title: _title,
      onGenerateTitle: (context) => AppLocalizations.of(context)!.appName,
      localizationsDelegates: AppLocalizations.localizationsDelegates,
      supportedLocales: AppLocalizations.supportedLocales,
      debugShowCheckedModeBanner: false,
      theme: _getThemeData(),
      home: LoadingOverlay(child: const LoginPage()),
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
