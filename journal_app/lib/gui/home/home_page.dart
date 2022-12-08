import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

import 'package:journal_mobile_app/gui/home/my_vehicles_component.dart';
import 'package:journal_mobile_app/gui/vehicle/new_vehicle_page.dart';

class HomePage extends ConsumerWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return _getPageBody(context, ref);
  }

  Widget _getPageBody(BuildContext context, WidgetRef ref) {
    return Scaffold(
      // appBar: AppBar(
      //   title: const Text(
      //     "Hello Brian", //TODO: Translate and get the User nama/nickname here
      //     style: TextStyle(color: Colors.black),
      //   ),
      //   backgroundColor: Colors.transparent,
      //   shadowColor: Colors.transparent,
      // ),
      body: SafeArea(
        child: Column(
          children: [
            const Padding(
              padding: EdgeInsets.all(10.0),
              child: SizedBox(
                width: double.infinity,
                child: Text(
                  "Hello Brian!",
                  style: TextStyle(fontSize: 30, fontWeight: FontWeight.bold),
                ),
              ),
            ),
            const MyVehiclesComponent(),
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
        ),
      ),
    );
  }
}
