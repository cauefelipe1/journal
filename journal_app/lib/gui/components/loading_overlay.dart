import 'dart:ui';

import 'package:flutter/material.dart';

class LoadingOverlay extends StatelessWidget {
  final Widget child;
  final Duration delay;
  late final ValueNotifier<bool> _isLoadingNotifier;

  LoadingOverlay({
    super.key,
    required this.child,
    this.delay = const Duration(milliseconds: 500),
  }) {
    _isLoadingNotifier = ValueNotifier(false);
  }

  void show() {
    _isLoadingNotifier.value = true;
  }

  void hide() {
    _isLoadingNotifier.value = false;
  }

  static LoadingOverlay of(BuildContext context) {
    var overlay = context.findAncestorWidgetOfExactType<LoadingOverlay>();

    if (overlay == null) {
      throw Exception("LoadingOverlay not found.");
    }

    return overlay;
  }

  @override
  Widget build(BuildContext context) {
    return ValueListenableBuilder<bool>(
        valueListenable: _isLoadingNotifier,
        child: child,
        builder: (context, isLoading, child) {
          return Stack(
            children: [
              child!,
              if (isLoading)
                BackdropFilter(
                  filter: ImageFilter.blur(sigmaX: 4.0, sigmaY: 4.0),
                  child: const Opacity(
                    opacity: 0.8,
                    child: ModalBarrier(
                      dismissible: false,
                      color: Colors.black,
                    ),
                  ),
                ),
              if (isLoading)
                Center(
                  child: FutureBuilder(
                      future: Future.delayed(delay),
                      builder: (context, snapshot) {
                        return snapshot.connectionState == ConnectionState.done ? const CircularProgressIndicator() : const SizedBox();
                      }),
                ),
            ],
          );
        });
  }
}
