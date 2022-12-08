import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class WelcomeComponent extends ConsumerWidget {
  const WelcomeComponent({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return _getBody(context, ref);
  }

  Widget _getBody(BuildContext context, WidgetRef ref) {
    return const SizedBox(
      height: 200,
      width: double.infinity,
      child: Padding(
        padding: EdgeInsets.fromLTRB(10, 0, 10, 10),
        child: Card(
          child: Padding(
            padding: EdgeInsets.all(10.0),
            child: Text("Hello Brian!"),
          ),
        ),
      ),
    );
  }
}
