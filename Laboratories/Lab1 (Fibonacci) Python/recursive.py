import time
import matplotlib.pyplot as plt
from matplotlib.ticker import ScalarFormatter

def fibonacci_recursive(n):
    if n <= 1:
        return n
    
    return fibonacci_recursive(n - 1) + fibonacci_recursive(n - 2)

# --- Empirical Analysis Setup ---
n_values = [5, 7, 10, 12, 15, 17, 20, 22, 25, 27, 30, 32, 35, 37, 40, 42, 45]

avg_times = []

# --- Terminal Output (Horizontal with 3 Repetitions) ---
print(f"{'n':<7} | {'Repetition 1':<12} | {'Repetition 2':<12} | {'Repetition 3':<12} | {'Average':<12}")
print("-" * 75)

for n in n_values:
    runs = []
    for _ in range(3):
        start = time.perf_counter()
        fibonacci_recursive(n)
        end = time.perf_counter()
        runs.append(end - start)
    
    avg = sum(runs) / 3
    avg_times.append(avg)
    
    # Print in the requested horizontal format
    print(f"{n:<7} | {runs[0]:.8f} | {runs[1]:.8f} | {runs[2]:.8f} | {avg:.8f}")

# --- Graph Generation ---
plt.figure(figsize=(10, 6))
plt.plot(n_values, avg_times, label='Recursive Method', marker='s', color='blue', linewidth=1.5)

plt.title('Empirical Analysis: Recursive (Average of 3 Runs)')
plt.xlabel('n-th Fibonacci Term')
plt.ylabel('Time (s)')

# Force the Y-axis to stay in 0.000... format
plt.gca().yaxis.set_major_formatter(ScalarFormatter(useOffset=False))
plt.ticklabel_format(style='plain', axis='y')

plt.grid(True, linestyle='--', alpha=0.6)
plt.legend()
plt.xticks([ 5, 10, 15, 20, 25, 30, 35, 40, 45])

plt.tight_layout()
plt.show()