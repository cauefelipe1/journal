sealed class Result<S, E> {
  const Result();
}

final class Success<S, E> extends Result<S, E> {
  final S value;

  const Success(this.value);
}

final class Failure<S, E> extends Result<S, E> {
  final E value;

  const Failure(this.value);
}
