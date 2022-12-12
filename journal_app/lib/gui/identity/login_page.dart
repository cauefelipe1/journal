import 'package:flutter/material.dart';
import 'package:journal_mobile_app/features/identity/identity_service.dart';
import 'package:journal_mobile_app/gui/base/base_page.dart';
import 'package:journal_mobile_app/gui/home/home_page.dart';
import 'package:journal_mobile_app/models/identity.dart';

import 'package:flutter_gen/gen_l10n/app_localizations.dart';

class LoginPage extends BasePage {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends BasePageState<LoginPage> {
  TextEditingController emailController = TextEditingController();
  TextEditingController passwordController = TextEditingController();
  bool _passwordVisible = false;
  bool _showPasswordButtonVisible = false;

  @override
  Widget widgetBuild(BuildContext context, AppLocalizations l10n) {
    return Scaffold(
      backgroundColor: Colors.grey[200],
      body: SafeArea(
        child: SingleChildScrollView(
          child: Center(
            child: Column(
              children: <Widget>[
                const SizedBox(height: 70),
                const Icon(
                  Icons.car_repair,
                  size: 150,
                ),
                Text(
                  l10n.appName,
                  style: const TextStyle(
                    fontWeight: FontWeight.w800,
                    fontSize: 35,
                  ),
                ),
                const SizedBox(height: 20),
                Text(
                  l10n.loginPageHeader,
                  style: const TextStyle(fontSize: 30),
                ),
                const SizedBox(height: 20),
                //Username field
                Padding(
                  padding: const EdgeInsets.fromLTRB(15, 10, 15, 0),
                  child: TextField(
                    controller: emailController,
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
                      hintText: l10n.usernameTextFieldHint,
                    ),
                  ),
                ),
                //Password field
                Padding(
                  padding: const EdgeInsets.fromLTRB(15, 10, 15, 0),
                  child: TextField(
                    keyboardType: TextInputType.visiblePassword,
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
                          icon: _passwordVisible ? const Icon(Icons.visibility_off) : const Icon(Icons.visibility),
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
                      hintText: l10n.passwordTextFieldHint,
                    ),
                  ),
                ),
                TextButton(
                  onPressed: () {},
                  child: Text(l10n.forgotPasswordButton),
                ),
                //Login button

                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 15.0),
                  child: ConstrainedBox(
                    constraints: const BoxConstraints.tightFor(width: double.infinity, height: 60),
                    child: ElevatedButton(
                      onPressed: () => _loginUser(context),
                      child: Text(
                        l10n.loginButton,
                        style: const TextStyle(
                          fontSize: 20,
                        ),
                      ),
                    ),
                  ),
                ),

                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: <Widget>[
                    Text(l10n.doesNotHaveAccountText),
                    TextButton(
                      child: Text(
                        l10n.signUpButton,
                        style: const TextStyle(fontSize: 20),
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
      ),
    );
  }

  Future<void> _loginUser(BuildContext context) async {
    var nv = Navigator.of(context);

    var loginInput = UserLoginInput(email: emailController.text, password: passwordController.text);

    var identityDS = IdentityService();
    var loginResult = await identityDS.loginUser(loginInput);

    if (loginResult.errors != null && loginResult.errors!.isNotEmpty) {
      String? error = loginResult.errors?.join("/n");
      await _showLoginErrorDialog(error!);

      return;
    }

    nv.pushReplacement(MaterialPageRoute(builder: (context) => const HomePage()));
    //var brands = await VehicleDataService().getAllBrands();
    //debugPrint(brands.toString());
  }

  Future<void> _showLoginErrorDialog(String errorMessage) async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false, // user must tap button!
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(AppLocalizations.of(context)!.notAbleToLogin),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                Text(errorMessage),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: Text(AppLocalizations.of(context)!.ok),
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
