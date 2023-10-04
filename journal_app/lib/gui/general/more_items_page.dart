import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/gui/common/error_dialog.dart';
import 'package:journal_mobile_app/gui/components/loading_overlay.dart';
import 'package:journal_mobile_app/gui/identity/login_controller.dart';
import 'package:journal_mobile_app/gui/identity/login_page.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';

class MoreItemsPage extends ConsumerWidget {
  late final ScrollController controller;

  MoreItemsPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 15.0),
        child: ConstrainedBox(
          constraints: const BoxConstraints.tightFor(width: double.infinity, height: 60),
          child: ElevatedButton(
            onPressed: () => _logout(ref),
            child: Text(
              context.l10n.logoutOption,
              style: const TextStyle(
                fontSize: 20,
              ),
            ),
          ),
        ),
      ),
    );
  }

  Future<void> _logout(WidgetRef ref) async {
    //TODO: Work on the failure path for logout.

    final context = ref.context;
    var overlay = LoadingOverlay.of(context);

    overlay.show();

    var nv = Navigator.of(context);

    bool isLogouted = false;
    String error = "Error during the logout, try again."; //TODO: Translate it

    try {
      isLogouted = await ref.read(loginControllerProvider).logout();
    } catch (e) {
      error = e.toString();
    } finally {
      overlay.hide();
    }

    if (!isLogouted) {
      await ErrorDialog.show(ref.context, context.l10n.notAbleToLogout, error);

      return;
    }

    nv.pushReplacement(MaterialPageRoute(builder: (context) => LoadingOverlay(child: LoginPage())));
  }
}
