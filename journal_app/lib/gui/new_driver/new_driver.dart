import 'package:flutter/material.dart';

class NewDriverPage extends StatefulWidget {
  const NewDriverPage({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _NewDriverState();
}

class _NewDriverState extends State<NewDriverPage> {
  TextEditingController txtFirstName = TextEditingController();
  TextEditingController txtLastName = TextEditingController();
  TextEditingController txtCountryId = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("My data"),
      ),
      body: _internalPageLayoutCreation(),
    );
  }

  Widget _internalPageLayoutCreation() {
    return Padding(
      padding: const EdgeInsets.all(15),
      child: ListView(
        children: <Widget>[
          TextField(
            controller: txtFirstName,
            decoration: const InputDecoration(
              border: UnderlineInputBorder(),
              labelText: "First name",
            ),
          ),
          TextField(
            controller: txtLastName,
            decoration: const InputDecoration(
              border: UnderlineInputBorder(),
              labelText: "Last name",
            ),
          ),
          TextField(
            controller: txtCountryId,
            decoration: const InputDecoration(
              border: UnderlineInputBorder(),
              labelText: "Country",
            ),
          ),
        ],
      ),
    );
  }
}
