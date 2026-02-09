class UserPreferences {
  final int minAgePref;
  final int maxAgePref;
  final String interestedIn;

  UserPreferences({
    required this.minAgePref,
    required this.maxAgePref,
    required this.interestedIn,
  });

  factory UserPreferences.fromJson(Map<String, dynamic> json) {
    return UserPreferences(
      minAgePref: json['minAgePref'],
      maxAgePref: json['maxAgePref'],
      interestedIn: json['interestedIn'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'minAgePref': minAgePref,
      'maxAgePref': maxAgePref,
      'interestedIn': interestedIn,
    };
  }
}
