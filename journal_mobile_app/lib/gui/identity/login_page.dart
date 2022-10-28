import 'package:flutter/material.dart';
import 'package:journal_mobile_app/features/identity/identity_data_service.dart';
import 'package:journal_mobile_app/models/identity.dart';
import 'package:journal_mobile_app/models/vehicle.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key, required this.title});

  final String title;

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  @override
  Widget build(BuildContext context) {
    TextEditingController emailController = TextEditingController();
    TextEditingController passwordController = TextEditingController();

    VehicleBrandModel? abs;
    return Padding(
      padding: const EdgeInsets.all(10),
      child: ListView(
        children: <Widget>[
          Container(
            alignment: Alignment.center,
            padding: const EdgeInsets.all(10),
            child: const Text(
              'Journal App', //TODO: Translate
              style: TextStyle(
                  color: Colors.blue,
                  fontWeight: FontWeight.w500,
                  fontSize: 30),
            ),
          ),
          Container(
            alignment: Alignment.center,
            padding: const EdgeInsets.all(10),
            child: const Text(
              'Sign In', //TODO: Translate
              style: TextStyle(fontSize: 30),
            ),
          ),
          Container(
            padding: const EdgeInsets.all(10),
            child: TextField(
              controller: emailController,
              decoration: const InputDecoration(
                border: OutlineInputBorder(),
                labelText: 'Email', //TODO: Translate
              ),
            ),
          ),
          Container(
            padding: const EdgeInsets.fromLTRB(10, 10, 10, 0),
            child: TextField(
              obscureText: true,
              controller: passwordController,
              decoration: const InputDecoration(
                border: OutlineInputBorder(),
                labelText: 'Password', //TODO: Translate
              ),
            ),
          ),
          TextButton(
            onPressed: () {},
            child: const Text('Forgot Password'), //TODO: Translate
          ),
          Container(
            height: 50,
            padding: const EdgeInsets.fromLTRB(10, 0, 10, 0),
            child: ElevatedButton(
              child: const Text('Login'), //TODO: Translate
              onPressed: () async {
                var loginInput = UserLoginInput(
                    email: emailController.text,
                    password: passwordController.text);

                var loginResult = await IdentityDataService().login(loginInput);
                debugPrint(loginResult.toJson().toString());
              },
            ),
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              const Text('Does not have an account?'), //TODO: Translate
              TextButton(
                child: const Text(
                  'Sign Up', //TODO: Translate
                  style: TextStyle(fontSize: 20),
                ),
                onPressed: () {
                  //Call the registration page
                },
              )
            ],
          )
        ],
      ),
    );
  }
}
