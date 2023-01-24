import 'package:flutter/material.dart';
import 'package:journal_mobile_app/gui/components/animated_navbar/bottom_navbar_notifier.dart';

class AnimatedBottomNavBar extends StatefulWidget {
  final List<BottomNavigationBarItem> items;
  final BottomNavbarNotifier model;
  final ValueChanged onItemTapped;

  const AnimatedBottomNavBar({
    super.key,
    required this.items,
    required this.model,
    required this.onItemTapped,
  });

  @override
  State<AnimatedBottomNavBar> createState() => _AnimatedBottomNavBarState();
}

class _AnimatedBottomNavBarState extends State<AnimatedBottomNavBar> with SingleTickerProviderStateMixin {
  bool isHidden = false;
  late Animation<double> animation;
  late AnimationController _controller;

  @override
  void didUpdateWidget(covariant AnimatedBottomNavBar oldWidget) {
    if (widget.model.hideBottomNavBar != isHidden) {
      if (!isHidden) {
        _showBottomNavBar();
      } else {
        _hideBottomNavBar();
      }
      isHidden = !isHidden;
    }

    super.didUpdateWidget(oldWidget);
  }

  @override
  void initState() {
    super.initState();

    _controller = AnimationController(
      duration: const Duration(milliseconds: 500),
      vsync: this,
    )..addListener(() {});

    animation = Tween(
      begin: 0.0,
      end: 100.0,
    ).animate(_controller);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: animation,
      builder: (context, child) {
        return Transform.translate(
          offset: Offset(0, animation.value),
          child: Container(
            decoration: BoxDecoration(
              boxShadow: [
                BoxShadow(
                  color: Colors.black.withOpacity(0.2),
                  blurRadius: 12,
                  spreadRadius: 2,
                  offset: const Offset(2, -2),
                ),
              ],
            ),
            child: BottomNavigationBar(
              type: BottomNavigationBarType.fixed,
              currentIndex: widget.model.index,
              onTap: (value) => widget.onItemTapped(value),
              elevation: 16.0,
              showUnselectedLabels: true,
              unselectedItemColor: Colors.grey,
              selectedItemColor: Colors.teal,
              items: widget.items,
            ),
          ),
        );
      },
    );
  }

  void _hideBottomNavBar() {
    _controller.reverse();
  }

  void _showBottomNavBar() {
    _controller.forward();
  }
}
