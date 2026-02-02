import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class OrientationScreen extends StatefulWidget {
  const OrientationScreen({super.key});

  @override
  State<OrientationScreen> createState() => _OrientationScreenState();
}

class _OrientationScreenState extends State<OrientationScreen> {
  int iAmIndex = 0;
  int lookingForIndex = 1;

  int minAge = 18;
  int maxAge = 30;

  final List<String> iAmOptions = ["Man", "Woman", "Other"];
  final List<String> lookingOptions = ["Men", "Women", "Everyone"];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        elevation: 0,
        centerTitle: true,
        backgroundColor: Colors.white,
        title: const Text(
          "Your preferences",
          style: TextStyle(
            fontSize: 22,
            color: Colors.black,
            fontWeight: FontWeight.bold,
          ),
        ),
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 16),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              // ------------------------ I AM ------------------------
              const Text(
                "I am a",
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.w700),
              ),
              const SizedBox(height: 12),

              _buildTinderSelector(
                selectedIndex: iAmIndex,
                labels: iAmOptions,
                onChanged: (index) => setState(() => iAmIndex = index),
              ),

              const SizedBox(height: 40),

              // ------------------------ LOOKING FOR ------------------------
              const Text(
                "I'm looking for",
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.w700),
              ),
              const SizedBox(height: 12),

              _buildTinderSelector(
                selectedIndex: lookingForIndex,
                labels: lookingOptions,
                onChanged: (index) => setState(() => lookingForIndex = index),
              ),

              const SizedBox(height: 40),

              // ------------------------ AGE RANGE ------------------------
              const Text(
                "Preferred age range",
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.w700),
              ),
              const SizedBox(height: 12),

              GestureDetector(
                onTap: _openAgePicker,
                child: Container(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 20,
                    vertical: 18,
                  ),
                  decoration: BoxDecoration(
                    color: Colors.grey.shade100,
                    borderRadius: BorderRadius.circular(18),
                    border: Border.all(color: Colors.grey.shade300),
                  ),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(
                        "$minAge – $maxAge",
                        style: const TextStyle(
                          fontSize: 20,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                      const Icon(Icons.chevron_right, size: 30),
                    ],
                  ),
                ),
              ),

              const Spacer(),

              // ------------------------ CONTINUE BUTTON ------------------------
              SizedBox(
                width: double.infinity,
                child: ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.pinkAccent,
                    padding: const EdgeInsets.symmetric(vertical: 16),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(16),
                    ),
                  ),
                  onPressed: () {
                    context.go('/interest-screen');
                  },
                  child: const Text(
                    "Continue",
                    style: TextStyle(
                      fontSize: 18,
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ),

              const SizedBox(height: 20),
            ],
          ),
        ),
      ),
    );
  }

  // -----------------------------------------------------------
  // TINDER STYLE SELECTOR
  // -----------------------------------------------------------
  Widget _buildTinderSelector({
    required int selectedIndex,
    required List<String> labels,
    required ValueChanged<int> onChanged,
  }) {
    return Row(
      children: List.generate(labels.length, (index) {
        final isActive = index == selectedIndex;

        return Expanded(
          child: GestureDetector(
            onTap: () => onChanged(index),
            child: AnimatedContainer(
              duration: const Duration(milliseconds: 200),
              margin: const EdgeInsets.symmetric(horizontal: 6),
              padding: const EdgeInsets.symmetric(vertical: 16),
              decoration: BoxDecoration(
                color: isActive ? Colors.pinkAccent : Colors.grey.shade200,
                borderRadius: BorderRadius.circular(16),
              ),
              child: Center(
                child: Text(
                  labels[index],
                  style: TextStyle(
                    fontSize: 16,
                    color: isActive ? Colors.white : Colors.black87,
                    fontWeight: FontWeight.w700,
                  ),
                ),
              ),
            ),
          ),
        );
      }),
    );
  }

  // -----------------------------------------------------------
  // AGE PICKER BOTTOM SHEET
  // -----------------------------------------------------------
  void _openAgePicker() {
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(top: Radius.circular(24)),
      ),
      builder: (context) {
        int tempMin = minAge;
        int tempMax = maxAge;

        return StatefulBuilder(
          builder: (context, setModalState) {
            return Container(
              height: 360,
              padding: const EdgeInsets.only(top: 20),
              child: Column(
                children: [
                  Container(
                    width: 60,
                    height: 6,
                    decoration: BoxDecoration(
                      color: Colors.grey.shade400,
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),

                  const SizedBox(height: 20),

                  const Text(
                    "Select Age Range",
                    style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
                  ),

                  const SizedBox(height: 20),

                  Expanded(
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                      children: [
                        // MIN PICKER
                        _agePicker(
                          initial: tempMin,
                          onChanged: (v) => setModalState(() => tempMin = v),
                        ),

                        // MAX PICKER
                        _agePicker(
                          initial: tempMax,
                          onChanged: (v) => setModalState(() => tempMax = v),
                        ),
                      ],
                    ),
                  ),

                  // DONE BUTTON
                  Padding(
                    padding: const EdgeInsets.all(16),
                    child: ElevatedButton(
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.pinkAccent,
                        minimumSize: const Size(double.infinity, 52),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(16),
                        ),
                      ),
                      onPressed: () {
                        setState(() {
                          minAge = tempMin;
                          maxAge = tempMax;
                        });
                        Navigator.pop(context);
                      },
                      child: const Text(
                        "Done",
                        style: TextStyle(fontSize: 18, color: Colors.white),
                      ),
                    ),
                  ),
                ],
              ),
            );
          },
        );
      },
    );
  }

  // -----------------------------------------------------------
  // INDIVIDUAL PICKER
  // -----------------------------------------------------------
  Widget _agePicker({
    required int initial,
    required ValueChanged<int> onChanged,
  }) {
    return SizedBox(
      width: 120,
      child: CupertinoPicker(
        itemExtent: 38,
        scrollController: FixedExtentScrollController(
          initialItem: initial - 18,
        ),
        onSelectedItemChanged: (value) => onChanged(value + 18),
        children: List.generate(
          83,
          (i) => Center(
            child: Text("${i + 18}", style: const TextStyle(fontSize: 20)),
          ),
        ),
      ),
    );
  }
}
