import 'package:flutter/material.dart';
import 'package:tinderclone/features/auth/login/screens/login_screen.dart';
import 'package:tinderclone/features/auth/registration/screens/interests_screen.dart';
import 'package:tinderclone/features/auth/registration/screens/orientation_screen.dart';
import 'package:tinderclone/features/auth/registration/screens/profile_creation_screen.dart';
import 'package:tinderclone/features/auth/registration/screens/registration_screen.dart';
import 'package:tinderclone/features/discovery/home/discovery_screen.dart';
import 'package:tinderclone/router.dart';

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(routerConfig: AppRouter.router);
  }
}
