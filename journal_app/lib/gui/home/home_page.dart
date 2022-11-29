import 'package:flutter/material.dart';

import 'package:journal_mobile_app/gui/base/base_page.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package:journal_mobile_app/gui/vehicle/new_vehicle_page.dart';

class HomePage extends BasePage {
  const HomePage({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _HomePageState();
}

class _HomePageState extends BasePageState<HomePage> {
  @override
  Widget widgetBuild(BuildContext context, AppLocalizations l10n) {
    return Scaffold(
        appBar: AppBar(
          title: const Text("Vehicle's life"), //TODO Translate
        ),
        body: Column(
          children: [
            const Text("I'm a page"),
            ElevatedButton(
              onPressed: () => {
                Navigator.of(context).push(MaterialPageRoute(builder: (context) => const NewVehiclePage())),
              },
              child: const Text(
                "New vehicle",
                style: TextStyle(
                  fontSize: 20,
                ),
              ),
            ),
          ],
        ));
  }
}
