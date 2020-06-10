from sklearn.feature_extraction.text import CountVectorizer
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
import random
from sklearn.metrics import accuracy_score


def main():
    data = []
    data_labels = []
    with open("./pos_tweets.txt") as f:
        for i in f:
            data.append(i)
            data_labels.append('pos')

    with open("neg_tweets.txt", encoding="utf-8") as f:
        print(f.encoding)
        for i in f:
            data.append(i)
            data_labels.append('neg')

    vectorizer = CountVectorizer(lowercase=False, analyzer='word')
    features = vectorizer.fit_transform(data)
    features_nd = features.toarray()
    X_train, X_test, y_train, y_test = train_test_split(
        features_nd,
        data_labels,
        train_size=0.80,
        random_state=1234)
    logModel = LogisticRegression()
    logModel = logModel.fit(X=X_train, y=y_train)
    y_pred = logModel.predict(X_test)

    j = random.randint(0, len(X_test) - 7)

    for i in range(j, j + 7):
        print(y_pred[0])
        ind = features_nd.tolist().index(X_test[i].tolist())
        print(data[ind].strip())

    print(accuracy_score(y_test, y_pred))


if __name__ == "__main__":
    main()
