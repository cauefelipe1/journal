import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/gui/common/constants.dart';
import 'package:journal_mobile_app/gui/components/loading_overlay.dart';
import 'package:journal_mobile_app/gui/identity/login_controller.dart';
import 'package:journal_mobile_app/gui/landing/landing_page.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';
import 'package:journal_mobile_app/models/identity.dart';

class LoginPage extends ConsumerWidget {
  final TextEditingController emailController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();

  final ValueNotifier<bool> _passwordVisible2 = ValueNotifier(false);
  final ValueNotifier<bool> _showPasswordButtonVisible2 = ValueNotifier(false);

  LoginPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Scaffold(
      backgroundColor: Colors.grey[200],
      body: SafeArea(
        child: SingleChildScrollView(
          child: Center(
            child: Column(
              children: <Widget>[
                const SizedBox(height: 80),
                const Image(
                  image: AssetImage(UiConstants.imagesBasePath + "/app-logo.png"),
                  width: 330,
                ),
                // Text(
                //   context.l10n.appName,
                //   style: const TextStyle(
                //     fontWeight: FontWeight.w900,
                //     letterSpacing: 5,
                //     fontFamily: "Nunito",
                //     fontSize: 70,
                //   ),
                // ),
                //const SizedBox(height: 20),
                // Text(
                //   context.l10n.loginPageHeader,
                //   style: const TextStyle(fontSize: 80),
                // ),
                const SizedBox(height: 80),
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
                      hintText: context.l10n.usernameTextFieldHint,
                    ),
                  ),
                ),
                //Password field
                Padding(
                  padding: const EdgeInsets.fromLTRB(15, 10, 15, 0),
                  child: ValueListenableBuilder<bool>(
                      valueListenable: _passwordVisible2,
                      builder: (context, isVisible, child) {
                        return TextField(
                          keyboardType: TextInputType.visiblePassword,
                          onChanged: (value) {
                            _showPasswordButtonVisible2.value = value.isNotEmpty;
                          },
                          obscureText: !isVisible,
                          controller: passwordController,
                          decoration: InputDecoration(
                            prefixIcon: const Icon(Icons.lock_outline),
                            suffixIcon: ValueListenableBuilder<bool>(
                                valueListenable: _showPasswordButtonVisible2,
                                builder: (context, isButtonVisible, child) {
                                  return Visibility(
                                    visible: isButtonVisible,
                                    child: IconButton(
                                      icon: isVisible ? const Icon(Icons.visibility_off) : const Icon(Icons.visibility),
                                      onPressed: () {
                                        _passwordVisible2.value = !_passwordVisible2.value;
                                      },
                                    ),
                                  );
                                }),
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
                            hintText: context.l10n.passwordTextFieldHint,
                          ),
                        );
                      }),
                ),
                //Forgot password
                TextButton(
                  onPressed: () {},
                  child: Text(context.l10n.forgotPasswordButton),
                ),
                //Login button
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 15.0),
                  child: ConstrainedBox(
                    constraints: const BoxConstraints.tightFor(width: double.infinity, height: 60),
                    child: ElevatedButton(
                      onPressed: () => _loginUser(ref),
                      child: Text(
                        context.l10n.loginButton,
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
                    Text(context.l10n.doesNotHaveAccountText),
                    TextButton(
                      child: Text(
                        context.l10n.signUpButton,
                        style: const TextStyle(fontSize: 20),
                      ),
                      onPressed: () {
                        //TODO: Call the registration page
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

  Future<void> _loginUser(WidgetRef ref) async {
    final context = ref.context;
    var overlay = LoadingOverlay.of(context);
    overlay.show();

    var nv = Navigator.of(context);

    UserLoginResult loginResult;

    try {
      loginResult = await ref.read(loginControllerProvider).loginUser(
            emailController.text,
            passwordController.text,
          );
    } finally {
      overlay.hide();
    }

    if (loginResult.errors != null && loginResult.errors!.isNotEmpty) {
      String? error = loginResult.errors?.join("/n");
      await _showLoginErrorDialog(error!, ref);

      return;
    }

    nv.pushReplacement(MaterialPageRoute(builder: (context) => LandingPage()));
  }

  Future<void> _showLoginErrorDialog(String errorMessage, WidgetRef ref) async {
    return showDialog<void>(
      context: ref.context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(context.l10n.notAbleToLogin),
          content: SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                Text(errorMessage),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: Text(context.l10n.ok),
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
