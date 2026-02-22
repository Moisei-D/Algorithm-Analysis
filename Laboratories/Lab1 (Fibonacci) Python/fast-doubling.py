import time
import matplotlib.pyplot as plt
from matplotlib.ticker import ScalarFormatter

def fibonacci_fast_doubling(n):
    if n == 0:
        return (0, 1)
    else: 
        a, b = fibonacci_fast_doubling(n >> 1)
        c = a * (2 * b - a)
        d = a * a + b * b
        if n % 2 == 0:
            return (c, d)
        else: 
            return (d, c + d)
        
def fast_doubling(n):
    return fibonacci_fast_doubling(n)[0]

# --- Empirical Analysis Setup ---
n_values = [501, 631, 794, 1000, 1259, 1585, 1995, 2512, 
            3162, 3981, 5012, 6310, 7943, 10000, 12589, 15849]

avg_times = []

# --- Terminal Output (Horizontal with 3 Repetitions) ---
print(f"{'n':<7} | {'Repetition 1':<12} | {'Repetition 2':<12} | {'Repetition 3':<12} | {'Average':<12}")
print("-" * 75)

for n in n_values:
    runs = []
    for _ in range(3):
        start = time.perf_counter()
        fast_doubling(n)
        end = time.perf_counter()
        runs.append(end - start)
    
    avg = sum(runs) / 3
    avg_times.append(avg)
    
    # Print in the requested horizontal format
    print(f"{n:<7} | {runs[0]:.8f} | {runs[1]:.8f} | {runs[2]:.8f} | {avg:.8f}")

# --- Graph Generation ---
plt.figure(figsize=(10, 6))
plt.plot(n_values, avg_times, label='Fast Doubling Method', marker='s', color='blue', linewidth=1.5)

plt.title('Empirical Analysis: Fast Doubling (Average of 3 Runs)')
plt.xlabel('n-th Fibonacci Term')
plt.ylabel('Time (s)')

# Force the Y-axis to stay in 0.000... format
plt.gca().yaxis.set_major_formatter(ScalarFormatter(useOffset=False))
plt.ticklabel_format(style='plain', axis='y')

plt.grid(True, linestyle='--', alpha=0.6)
plt.legend()
plt.xticks([0, 2000, 4000, 6000, 8000, 10000, 12000, 14000, 16000])

plt.tight_layout()
plt.show()