import 'package:flutter/material.dart';
import 'package:journal_mobile_app/gui/components/animated_navbar/animated_bottom_navbar.dart';
import 'package:journal_mobile_app/gui/components/animated_navbar/bottom_navbar_notifier.dart';
import 'package:journal_mobile_app/gui/general/more_items_page.dart';
import 'package:journal_mobile_app/gui/home/home_page.dart';
import 'package:journal_mobile_app/gui/vehicle_event/select_vehicle_event_type.dart';
import 'package:journal_mobile_app/l10n/app_localization_context.dart';

class LandingPage extends StatefulWidget {
  const LandingPage({super.key});

  @override
  State<LandingPage> createState() => _LandingPageState();
}

class _LandingPageState extends State<LandingPage> with SingleTickerProviderStateMixin {
  late final List<Widget> _bototmBarPages;

  late final BottomNavbarNotifier _bottomNavbarNotifier;
  final homeKey = GlobalKey<NavigatorState>();

  late AnimationController _controller;
  late Animation<double> _fadeTransition;

  @override
  void initState() {
    _bottomNavbarNotifier = BottomNavbarNotifier();
    _bototmBarPages = [
      HomePage(
        bottomNavbarListener: _bottomNavbarNotifier,
        onNavigateRequest: _navigateRequest,
      ),
      SelectVehicleEventType(),
      MoreIemsPage(),
    ];

    _controller = AnimationController(
      duration: const Duration(milliseconds: 500),
      vsync: this,
    )..addListener(() {});

    _fadeTransition = Tween(
      begin: 0.5,
      end: 1.0,
    ).animate(CurvedAnimation(
      parent: _controller,
      curve: Curves.fastOutSlowIn,
    ));

    _controller.reset();
    _controller.forward();

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Material(
      child: AnimatedBuilder(
        animation: _bottomNavbarNotifier,
        builder: (context, child) {
          return Stack(
            children: [
              IndexedStack(
                index: _bottomNavbarNotifier.index,
                children: [
                  for (int i = 0; i < _bototmBarPages.length; i++)
                    FadeTransition(
                      opacity: _fadeTransition,
                      child: _bototmBarPages[i],
                    ),
                ],
              ),
              Positioned(
                bottom: 0,
                left: 0,
                right: 0,
                child: AnimatedBottomNavBar(
                  model: _bottomNavbarNotifier,
                  onItemTapped: (value) {
                    _bottomNavbarNotifier.index = value;
                    _controller.reset();
                    _controller.forward();
                  },
                  items: [
                    BottomNavigationBarItem(
                      icon: Icon(Icons.home),
                      label: context.l10n.homeText,
                    ),
                    BottomNavigationBarItem(
                      icon: Icon(Icons.add),
                      label: "",
                    ),
                    BottomNavigationBarItem(
                      icon: Icon(Icons.menu),
                      label: context.l10n.moreText,
                    ),
                  ],
                ),
              ),
            ],
          );
        },
      ),
    );
  }

  void _navigateRequest(String name) {
    _navigate(context, name, isRootNavigator: false);
  }

  Future<void> _navigate(
    BuildContext context,
    String route, {
    bool isRootNavigator = true,
    Map<String, dynamic>? arguments,
  }) {
    return Navigator.of(context, rootNavigator: isRootNavigator).pushNamed(route, arguments: arguments);
  }
}
