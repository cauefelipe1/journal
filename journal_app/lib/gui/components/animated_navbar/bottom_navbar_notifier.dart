import 'package:flutter/material.dart';

class BottomNavbarNotifier extends ChangeNotifier {
  int _index = 0;

  int get index => _index;

  set index(int value) {
    _index = value;
    notifyListeners();
  }

  bool _hideBottomNavBar = false;

  bool get hideBottomNavBar => _hideBottomNavBar;
  set hideBottomNavBar(bool value) {
    _hideBottomNavBar = value;
    notifyListeners();
  }
}
