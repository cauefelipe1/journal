import 'package:flutter/material.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';

class NewVehiclePage extends StatefulWidget {
  const NewVehiclePage({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _NewVehiclePageState();
}

class _NewVehiclePageState extends State<NewVehiclePage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(context.l10n.newVehiclePageHeader),
      ),
      body: _getPageBody(context),
    );
  }

  Widget _getPageBody(BuildContext context) {
    return Column(
      children: [
        _getVehicleTypeField(context),
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
            hintText: context.l10n.newVehicleModelField,
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
            hintText: context.l10n.newVehicleYearField,
          ),
        ),
      ],
    );
  }

  Widget _getVehicleTypeField(BuildContext context) {
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
        hintText: context.l10n.newVehicleTypeField,
      ),
    );
  }
}
