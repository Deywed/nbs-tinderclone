import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class BottomNavBar extends StatelessWidget {
  final int customPage;
  const BottomNavBar({super.key, required this.customPage});

  @override
  Widget build(BuildContext context) {
    return BottomNavigationBar(
      items: [
        BottomNavigationBarItem(
          icon: Icon(Icons.assignment_add),
          label: 'Discovery',
        ),
        BottomNavigationBarItem(icon: Icon(Icons.play_arrow), label: 'Chats'),
        BottomNavigationBarItem(icon: Icon(Icons.chat), label: 'Profile'),
      ],
      currentIndex: customPage,
      onTap: (index) {
        switch (index) {
          case 0:
            context.go('/discovery-screen');
            break;
          case 1:
            context.go('/chats-screen');
            break;
          case 2:
            context.go('/profile-screen');
            break;
        }
      },
    );
  }
}
