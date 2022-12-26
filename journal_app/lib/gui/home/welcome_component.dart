import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:journal_mobile_app/features/identity/identity_service.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';
import 'package:journal_mobile_app/models/user.dart';
import 'package:shimmer/shimmer.dart';

import 'package:flutter_gen/gen_l10n/app_localizations.dart';

var userInfo = FutureProvider.autoDispose((ref) => ref.watch(identityServiceProvider).getUserData());

class WelcomeComponent extends StatelessWidget {
  const WelcomeComponent({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.fromLTRB(20, 0, 20, 20),
      decoration: BoxDecoration(
        color: Colors.teal[300],
        borderRadius: const BorderRadius.only(
          bottomLeft: Radius.circular(25),
          bottomRight: Radius.circular(25),
        ),
      ),
      child: SafeArea(
        bottom: false,
        child: Consumer(
          builder: (context, ref, child) {
            return ref.watch(userInfo).when(
                  data: (data) => _getBody(data, context.l10n),
                  loading: _getShimmer,
                  error: (error, stackTrace) => Text(error.toString()),
                );
          },
        ),
      ),
    );
  }

  Widget _getBody(UserData? userData, AppLocalizations l10n) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.start,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          "${_getGreeting(l10n)},",
          style: const TextStyle(
            color: Colors.white,
            fontSize: 30,
          ),
        ),
        Text(
          userData?.displayName ?? "Unknown",
          style: const TextStyle(
            color: Colors.white,
            fontSize: 25,
          ),
        ),
      ],
    );
  }

  Widget _getShimmer() {
    return Shimmer.fromColors(
      baseColor: Colors.teal,
      highlightColor: Colors.white,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            height: 30,
            width: 300,
            color: Colors.grey,
          ),
          const SizedBox(
            height: 5,
          ),
          Container(
            height: 30,
            width: 200,
            color: Colors.grey,
          ),
        ],
      ),
    );
  }

  String _getGreeting(AppLocalizations l10n) {
    var now = DateTime.now(); //TODO: Create a datetime provider to make this testable

    if (now.hour >= 0 && (now.hour <= 11 && now.minute <= 59)) {
      return l10n.goodMorningText;
    } else if (now.hour >= 12 && (now.hour <= 17 && now.minute <= 59)) {
      return l10n.goodAfternoonText;
    } else {
      return l10n.goodEveningText;
    }
  }
}
