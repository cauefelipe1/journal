import 'package:flutter/material.dart';

class MoreIemsPage extends StatefulWidget {
  const MoreIemsPage({super.key});

  @override
  State<MoreIemsPage> createState() => _MoreIemsPageState();
}

class _MoreIemsPageState extends State<MoreIemsPage> {
  late final ScrollController controller;

  @override
  void initState() {
    controller = ScrollController();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: ListView.builder(
        itemCount: 50,
        controller: controller,
        itemBuilder: (context, index) {
          return Text("Item " + index.toString());
        },
      ),
    );
  }
}
