// MathLibrary.cpp : Defines the exported functions for the DLL.
#include "pch.h" // use stdafx.h in Visual Studio 2017 and earlier
#include <utility>
#include <limits.h>
#include "numeric-method.h"
#include <iostream>

// DLL internal state variables:
static unsigned long long previous_;  // Previous value, if any
static unsigned long long current_;   // Current sequence value
static unsigned index_;               // Current seq. position

static double yyyy_;   // third derivative of a function
static double yyy_; // second derivative of a function
static double yy_; // first derivative of a function
static double y_; // no derivative function

static double yyy0_; // second dericvative default value of function
static double yy0_; // second dericvative default value of function
static double y0_; // second dericvative default value of function
static double x0_; //
static double h_; // step
static int length_; // length

static double answer_[30000][6]; // table result in matrix

void difference_equation_init(
    const double yyyy,
    const double yyy,
    const double yy,
    const double y)
{
    yyyy_ = yyyy;
    yyy_ = yyy;
    yy_ = yy;
    y_ = y;
}

void default_value_init(
    const double yyy0,
    const double yy0,
    const double y0)
{
    yyy0_ = yyy0;
    yy0_ = yy0;
    y0_ = y0;
    x0_ = 0;
}

void step_init(const double h, const int length) {
    h_ = h;
    length_ = length;
}
 
bool difference_equation_solve() {
    
    answer_[0][0] = 0;
    answer_[0][1] = x0_;
    answer_[0][2] = y0_;
    answer_[0][3] = yy0_;
    answer_[0][4] = yyy0_;
    
    
    for (int i = 1; i <= length_; i++) {
        answer_[i][0] = i;
        answer_[i][1] = answer_[i - 1][1] + h_;
        answer_[i][2] = answer_[i - 1][2] + h_ * answer_[i - 1][3];
        answer_[i][3] = answer_[i - 1][3] + h_ * answer_[i - 1][4];
        answer_[i][4] = answer_[i - 1][4] + h_ * (-yyy_ * answer_[i - 1][4] - yy_ * answer_[i - 1][3] - y_ * answer_[i - 1][2]) / yyyy_;
    }

    return true;
}

double difference_equation_cell(int row, int column) {
    return answer_[row][column];
}

// Initialize a Fibonacci relation sequence
// such that F(0) = a, F(1) = b.
// This function must be called before any other function.
void fibonacci_init(
    const long long a,
    const long long b)
{
    index_ = 0;
    current_ = a;
    previous_ = b; // see special case when initialized
}

// Produce the next value in the sequence.
// Returns true on success, false on overflow.
bool fibonacci_next()
{
    // check to see if we'd overflow result or position
    if ((ULLONG_MAX - previous_ < current_) ||
        (UINT_MAX == index_))
    {
        return false;
    }

    // Special case when index == 0, just return b value
    if (index_ > 0)
    {
        // otherwise, calculate next sequence value
        previous_ += current_;
    }
    std::swap(current_, previous_);
    ++index_;
    return true;
}

// Get the current value in the sequence.
unsigned long long fibonacci_current()
{
    return current_;
}

// Get the current index position in the sequence.
unsigned fibonacci_index()
{
    return index_;
}