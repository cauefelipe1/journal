import 'package:flutter/material.dart';

import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package:journal_mobile_app/gui/base/base_page.dart';

class NewVehiclePage extends StatefulWidget {
  const NewVehiclePage({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _NewVehiclePageState();
}

class _NewVehiclePageState extends BasePageState<NewVehiclePage> {
  @override
  Widget widgetBuild(BuildContext context, AppLocalizations l10n) {
    return Scaffold(
      appBar: AppBar(
        title: Text(l10n.newVehiclePageHeader),
      ),
      body: _getPageBody(context, l10n),
    );
  }

  Widget _getPageBody(BuildContext context, AppLocalizations l10n) {
    return Column(
      children: [
        _getVehicleTypeField(context, l10n),
        TextField(
          controller: TextEditingController(),
          decoration: InputDecoration(
            prefixIcon: const Icon(Icons.person_outline),
            filled: true,
            fillColor: Colors.white,
            border: OutlineInputBorder(
              borderRadius: BorderRadius.circular(10),
            ),
            enabledBorder: OutlineInputBorder(
              borderRadius: BorderRadius.circular(10),
              borderSide: const BorderSide(color: Colors.white),
            ),
            hintText: l10n.newVehicleModelField,
          ),
        ),
        TextField(
          controller: TextEditingController(),
          keyboardType: TextInputType.number,
          decoration: InputDecoration(
            prefixIcon: const Icon(Icons.person_outline),
            filled: true,
            fillColor: Colors.white,
            border: OutlineInputBorder(
              borderRadius: BorderRadius.circular(10),
            ),
            enabledBorder: OutlineInputBorder(
              borderRadius: BorderRadius.circular(10),
              borderSide: const BorderSide(color: Colors.white),
            ),
            hintText: l10n.newVehicleYearField,
          ),
        ),
      ],
    );
  }

  Widget _getVehicleTypeField(BuildContext context, AppLocalizations l10n) {
    return TextField(
      controller: TextEditingController(),
      decoration: InputDecoration(
        prefixIcon: const Icon(Icons.person_outline),
        filled: true,
        fillColor: Colors.white,
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(10),
        ),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(10),
          borderSide: const BorderSide(color: Colors.white),
        ),
        hintText: l10n.newVehicleTypeField,
      ),
    );
  }
}
