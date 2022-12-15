import 'package:flutter/material.dart';

import 'package:journal_mobile_app/gui/home/my_vehicles_component.dart';
import 'package:journal_mobile_app/gui/home/welcome_component.dart';
import 'package:journal_mobile_app/gui/vehicle/new_vehicle_page.dart';

class HomePage extends StatelessWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return _getPageBody(context);
  }

  Widget _getPageBody(BuildContext context) {
    return Scaffold(
      // appBar: AppBar(
      //   title: const Text(
      //     "Hello Brian", //TODO: Translate and get the User name/nickname here
      //     style: TextStyle(color: Colors.black),
      //   ),
      //   backgroundColor: Colors.transparent,
      //   shadowColor: Colors.transparent,
      // ),
      backgroundColor: Colors.grey[200],
      body: Column(
        children: [
          const WelcomeComponent(),
          const Padding(
            padding: EdgeInsets.all(15),
            child: MyVehiclesComponent(),
          ),
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

      /*body: SafeArea(
        child: Column(
          children: [
            WelcomeComponent(),
            // const Padding(
            //   padding: EdgeInsets.all(10.0),
            //   child: SizedBox(
            //     width: double.infinity,
            //     child: WelcomeComponent(),
            //   ),
            // ),
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
      ),*/
    );
  }
}
