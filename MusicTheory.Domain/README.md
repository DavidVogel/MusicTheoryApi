# MusicTheory.Domain 

This project contains strong-typed models and enums representing musical concepts. This includes classes for `Note`, `Scale`, and `Chord`, enums for note names (`NoteName`), scale types (`ScaleType`), chord types (`ChordType`), and an interval representation. The domain layer also implements **enharmonic spelling logic** – ensuring that notes in scales and chords are labeled with the correct letter names (e.g. using `E#` instead of `F` where appropriate, so each letter A–G appears once in a scale).
