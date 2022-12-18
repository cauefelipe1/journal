import 'dart:ui';

import 'package:flutter/material.dart';

class LoadingOverlay extends StatefulWidget {
  final Widget child;
  final Duration delay;

  const LoadingOverlay({
    super.key,
    required this.child,
    this.delay = const Duration(milliseconds: 500),
  });

  static LoadingOverlayState of(BuildContext context) {
    var state = context.findAncestorStateOfType<LoadingOverlayState>();

    if (state == null) {
      throw Exception("LoadingOverlay not found.");
    }

    return state;
  }

  @override
  State<LoadingOverlay> createState() => LoadingOverlayState();
}

class LoadingOverlayState extends State<LoadingOverlay> {
  bool _isLoading = false;

  void show() {
    setState(() {
      _isLoading = true;
    });
  }

  void hide() {
    setState(() {
      _isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        widget.child,
        if (_isLoading)
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
        if (_isLoading)
          Center(
            child: FutureBuilder(
                future: Future.delayed(widget.delay),
                builder: (context, snapshot) {
                  return snapshot.connectionState == ConnectionState.done ? const CircularProgressIndicator() : const SizedBox();
                }),
          ),
      ],
    );
  }
}
