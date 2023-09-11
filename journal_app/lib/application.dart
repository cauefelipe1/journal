import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/features/driver/driver_data_service.dart';
import 'package:journal_mobile_app/features/identity/identity_service.dart';
import 'package:journal_mobile_app/gui/components/loading_overlay.dart';
import 'package:journal_mobile_app/gui/identity/login_page.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package:journal_mobile_app/gui/landing/landing_page.dart';
import 'package:journal_mobile_app/gui/vehicle/new_vehicle_page.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';
import 'package:journal_mobile_app/routes/routes_constants.dart';

var loggedUserInfoProvider = FutureProvider.autoDispose((ref) async {
  //var userInf = await ref.watch(identityServiceProvider).getUserData();
  var userInf = await ref.watch(driverDataServiceProvider).getLoggedDriver();

  ref.keepAlive();
  return userInf!;
});

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
      onGenerateRoute: _generateRoute,
      home: isLoggedIn ? LandingPage() : LoadingOverlay(child: LoginPage()),
    );
  }

  Route<dynamic>? _generateRoute(RouteSettings settings) {
    WidgetBuilder builder;
    switch (settings.name) {
      case RoutesConstants.newVehicle:
        builder = (BuildContext _) => NewVehiclePage();
        break;
      // As there is no route for invalid pages and also no reason for an invalid route as they are manages by code
      // the default also handles the root (/) route.
      default:
        builder = (context) => LandingPage();
    }

    return MaterialPageRoute(builder: builder, settings: settings);
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
