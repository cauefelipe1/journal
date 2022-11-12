import 'package:flutter/material.dart';
import 'package:journal_mobile_app/features/identity/identity_data_service.dart';
import 'package:journal_mobile_app/features/vehicle/vehicle_data_service.dart';
import 'package:journal_mobile_app/models/identity.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key, required this.title});

  final String title;

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  TextEditingController emailController = TextEditingController();
  TextEditingController passwordController = TextEditingController();
  bool _passwordVisible = false;
  bool _showPasswordButtonVisible = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[200],
      body: SafeArea(
        child: Center(
          child: Column(
            children: <Widget>[
              const Text(
                'Journal App', //TODO: Translate
                style: TextStyle(
                    color: Colors.blue,
                    fontWeight: FontWeight.w500,
                    fontSize: 30),
              ),
              const Text(
                'Sign In', //TODO: Translate
                style: TextStyle(fontSize: 30),
              ),
              //Username field
              Padding(
                padding: const EdgeInsets.fromLTRB(10, 10, 10, 0),
                child: TextField(
                  controller: emailController,
                  decoration: InputDecoration(
                    prefixIcon: const Icon(Icons.person_outline),
                    filled: true,
                    fillColor: Colors.white,
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(10),
                      borderSide: const BorderSide(color: Colors.pink),
                    ),
                    enabledBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(10),
                      borderSide: const BorderSide(color: Colors.white),
                    ),
                    hintText: "Username / Email", //TODO: Translate
                  ),
                ),
              ),
              //Password field
              Padding(
                padding: const EdgeInsets.fromLTRB(10, 10, 10, 0),
                child: TextField(
                  onChanged: (value) {
                    setState(() {
                      _showPasswordButtonVisible = value.isNotEmpty;
                    });
                  },
                  obscureText: !_passwordVisible,
                  controller: passwordController,
                  decoration: InputDecoration(
                    prefixIcon: const Icon(Icons.lock_outline),
                    suffixIcon: Visibility(
                      visible: _showPasswordButtonVisible,
                      child: IconButton(
                        icon: _passwordVisible
                            ? const Icon(Icons.visibility_off)
                            : const Icon(Icons.visibility),
                        onPressed: () {
                          setState(() {
                            _passwordVisible = !_passwordVisible;
                          });
                        },
                      ),
                    ),
                    filled: true,
                    fillColor: Colors.white,
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(10),
                      borderSide: const BorderSide(color: Colors.pink),
                    ),
                    enabledBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(10),
                      borderSide: const BorderSide(color: Colors.white),
                    ),
                    hintText: "Password", //TODO: Translate
                  ),
                ),
              ),
              TextButton(
                onPressed: () {},
                child: const Text('Forgot Password'), //TODO: Translate
              ),
              //Login button

              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 10.0),
                child: ConstrainedBox(
                  constraints: const BoxConstraints.tightFor(
                      width: double.infinity, height: 50),
                  child: ElevatedButton(
                    onPressed: _loginUser,
                    child: const Text('Login'),
                  ),
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
        ),
      ),
    );
  }

  Future<void> _loginUser() async {
    var loginInput = UserLoginInput(
        email: emailController.text, password: passwordController.text);

    var loginResult = await IdentityDataService().login(loginInput);

    if (loginResult.errors != null && loginResult.errors!.isNotEmpty) {
      String? error = loginResult.errors?.join("/n");
      await _showLoginErrorDialog(error!);

      return;
    }

    var brands = await VehicleDataService().getAllBrands();
    debugPrint(brands.toString());
  }

  Future<void> _showLoginErrorDialog(String errorMessage) async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false, // user must tap button!
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Not able to login'), //TODO: Translate
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                Text(errorMessage),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Ok'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }
}
