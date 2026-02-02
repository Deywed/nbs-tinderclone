import 'dart:ui';

import 'package:flutter/material.dart';

class UserProfile {
  final String name;
  final int age;
  final Color color;

  UserProfile({required this.name, required this.age, required this.color});
}

class DiscoveryScreen extends StatefulWidget {
  const DiscoveryScreen({super.key});

  @override
  State<DiscoveryScreen> createState() => _DiscoveryScreenState();
}

class _DiscoveryScreenState extends State<DiscoveryScreen>
    with SingleTickerProviderStateMixin {
  late List<UserProfile> users;

  Offset cardOffset = Offset.zero;
  double rotation = 0;

  static const swipeThreshold = 120;

  @override
  void initState() {
    super.initState();
    users = _fakeUsers();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey.shade100,
      body: Center(
        child: Stack(
          alignment: Alignment.center,
          children:
              users.asMap().entries.map((entry) {
                final index = entry.key;
                final user = entry.value;
                final isTop = index == users.length - 1;

                return isTop
                    ? _buildDraggableCard(user)
                    : _buildCard(user, scale: 0.95);
              }).toList(),
        ),
      ),
    );
  }

  Widget _buildDraggableCard(UserProfile user) {
    return GestureDetector(
      onPanUpdate: (details) {
        setState(() {
          cardOffset += details.delta;
          rotation = cardOffset.dx / 300;
        });
      },
      onPanEnd: (_) {
        if (cardOffset.dx > swipeThreshold) {
          _swipeRight();
        } else if (cardOffset.dx < -swipeThreshold) {
          _swipeLeft();
        } else {
          _resetCard();
        }
      },
      child: Transform.translate(
        offset: cardOffset,
        child: Transform.rotate(angle: rotation, child: _buildCard(user)),
      ),
    );
  }

  Widget _buildCard(UserProfile user, {double scale = 1}) {
    return Transform.scale(
      scale: scale,
      child: Container(
        width: 320,
        height: 440,
        decoration: BoxDecoration(
          color: user.color,
          borderRadius: BorderRadius.circular(24),
          boxShadow: const [
            BoxShadow(
              blurRadius: 16,
              color: Colors.black26,
              offset: Offset(0, 8),
            ),
          ],
        ),
        child: Align(
          alignment: Alignment.bottomLeft,
          child: Padding(
            padding: const EdgeInsets.all(20),
            child: Text(
              '${user.name}, ${user.age}',
              style: const TextStyle(
                fontSize: 28,
                fontWeight: FontWeight.bold,
                color: Colors.white,
              ),
            ),
          ),
        ),
      ),
    );
  }

  void _swipeRight() {
    _removeTopCard();
    debugPrint('LIKE ❤️');
  }

  void _swipeLeft() {
    _removeTopCard();
    debugPrint('NOPE ❌');
  }

  void _resetCard() {
    setState(() {
      cardOffset = Offset.zero;
      rotation = 0;
    });
  }

  void _removeTopCard() {
    setState(() {
      users.removeLast();
      cardOffset = Offset.zero;
      rotation = 0;
    });
  }

  List<UserProfile> _fakeUsers() {
    return [
      UserProfile(name: 'Anna', age: 24, color: Colors.pink),
      UserProfile(name: 'Marko', age: 27, color: Colors.blue),
      UserProfile(name: 'Sara', age: 22, color: Colors.orange),
      UserProfile(name: 'Luka', age: 29, color: Colors.green),
      UserProfile(name: 'Mia', age: 25, color: Colors.purple),
    ];
  }
}
