import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:journal_mobile_app/gui/components/animated_navbar/bottom_navbar_notifier.dart';
import 'package:journal_mobile_app/gui/components/my_vehicles/my_vehicles_component.dart';

import 'package:journal_mobile_app/gui/components/vehicle_history/vehicle_history_component.dart';
import 'package:journal_mobile_app/gui/components/welcome/welcome_component.dart';

class HomePage extends StatefulWidget {
  final BottomNavbarNotifier bottomNavbarListener;
  final Function(String)? onNavigateRequest;

  const HomePage({
    required this.bottomNavbarListener,
    required this.onNavigateRequest,
  });
  @override
  State<StatefulWidget> createState() => _HomePageState(bottomNavbarListener: bottomNavbarListener);
}

class _HomePageState extends State<HomePage> {
  _HomePageState({required this.bottomNavbarListener});

  String? currentVehicle = null;
  late final ScrollController _scrollController;
  final BottomNavbarNotifier bottomNavbarListener;

  @override
  void initState() {
    _scrollController = ScrollController();
    _addScrollListner();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[200],
      body: Column(
        children: [
          const WelcomeComponent(),
          Padding(
            padding: EdgeInsets.symmetric(horizontal: 15),
            child: MyVehiclesComponent(
              onCardPressed: (vehicleId) => setState(() => currentVehicle = vehicleId),
              onDataIsReady: (vehicleId) => setState(() => currentVehicle = vehicleId),
              onNavigateRequest: widget.onNavigateRequest,
            ),
          ),
          VehicleHistoryComponent(
            vehicleId: currentVehicle,
            scrollController: _scrollController,
          ),
        ],
      ),
    );
  }

  void _addScrollListner() {
    _scrollController.addListener(() {
      if (_scrollController.position.userScrollDirection == ScrollDirection.forward) {
        if (bottomNavbarListener.hideBottomNavBar) {
          bottomNavbarListener.hideBottomNavBar = false;
        }
      } else {
        if (!bottomNavbarListener.hideBottomNavBar) {
          bottomNavbarListener.hideBottomNavBar = true;
        }
      }
    });
  }
}
