// numeric-method.h - Contains declarations of math functions
#pragma once

#ifdef NUMERICMETHOD_EXPORTS
#define NUMERICMETHOD_API __declspec(dllexport)
#else
#define NUMERICMETHOD_API __declspec(dllimport)
#endif


extern "C" NUMERICMETHOD_API void difference_equation_init(
    const double yyyy,
    const double yyy,
    const double yy,
    const double y);

extern "C" NUMERICMETHOD_API void default_value_init(
    const double yyy0,
    const double yy0,
    const double y0);

extern "C" NUMERICMETHOD_API void step_init(const double h, const int length);

extern "C" NUMERICMETHOD_API bool difference_equation_solve();

extern "C" NUMERICMETHOD_API double difference_equation_cell(int row, int column);
// The Fibonacci recurrence relation describes a sequence F
// where F(n) is { n = 0, a
//               { n = 1, b
//               { n > 1, F(n-2) + F(n-1)
// for some initial integral values a and b.
// If the sequence is initialized F(0) = 1, F(1) = 1,
// then this relation produces the well-known Fibonacci
// sequence: 1, 1, 2, 3, 5, 8, 13, 21, 34, ...

// Initialize a Fibonacci relation sequence
// such that F(0) = a, F(1) = b.
// This function must be called before any other function.
extern "C" NUMERICMETHOD_API void fibonacci_init(
    const unsigned long long a, const unsigned long long b);

// Produce the next value in the sequence.
// Returns true on success and updates current value and index;
// false on overflow, leaves current value and index unchanged.
extern "C" NUMERICMETHOD_API bool fibonacci_next();

// Get the current value in the sequence.
extern "C" NUMERICMETHOD_API unsigned long long fibonacci_current();

// Get the position of the current value in the sequence.
extern "C" NUMERICMETHOD_API unsigned fibonacci_index();