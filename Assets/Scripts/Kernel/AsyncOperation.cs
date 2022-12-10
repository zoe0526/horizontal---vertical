namespace FullHouseCasino.kernel.system.thread {
    public class AsyncOperation {
        private bool _is_done;
        public bool is_done { get { return _is_done; } }
        public void done() {
            _is_done = true;
        }

        private float _progress;
        public float progress { get { return _progress; } }

        public void update_progress(float progress) {
            if (progress >= 1f) {
                _progress = 1f;
                _is_done = true;
            } else {
                _progress = progress;
            }
        }

        public void reset() {
            _progress = 0f;
            _is_done = false;
        }
    }
}
